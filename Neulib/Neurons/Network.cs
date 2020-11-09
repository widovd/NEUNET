using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using Neulib.Exceptions;
using Neulib.Numerics;
using Neulib.Serializers;
using static Neulib.Extensions.FloatExtensions;
using System.Collections;
using static System.Math;

namespace Neulib.Neurons
{
    public class Network : Layer, IEnumerable, IEnumerator
    {
        // ----------------------------------------------------------------------------------------
        #region Layer properties

        public override SingleLayer OutputLayer { get => Last?.OutputLayer; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network properties

        public Layer First { get; set; }

        public Layer Last
        {
            get
            {
                Layer layer = First;
                if (layer == null) return null;
                while (layer.Next != null) layer = layer.Next;
                return layer;
            }
        }

        public int Count
        {
            get
            {
                Layer layer = First;
                int count = 0;
                while (layer != null) { layer = layer.Next; count++; }
                return count;
            }
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Network()
        {
        }

        public Network(Stream stream, BinarySerializer serializer)
        {
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Add((Layer)stream.ReadValue(serializer));
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IEnumerable

        private Layer _layer;

        public bool MoveNext()
        {
            if (_layer == null)
                _layer = First;
            else
                _layer = _layer.Next;
            return (_layer != null);
        }
        public void Reset()
        {
            _layer = null;
        }
        public object Current
        {
            get { return _layer; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network

        public void Add(Layer layer)
        {
            Layer last = Last;
            if (last == null)
            {
                First = layer;
            }
            else
            {
                last.Next = layer;
                layer.Previous = last;
            }
        }

        public void Clear()
        {
            First.Remove();
            First = null;
        }

        public void ForEach(Action<Layer> action)
        {
            Layer layer = First;
            while (layer != null)
            {
                action(layer);
                layer = layer.Next;
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Network value = o as Network ?? throw new InvalidTypeException(o, nameof(Network), 473835);
            Clear();
            value.ForEach(layer => Add((Layer)layer.Clone()));
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteInt(Count);
            ForEach(layer => stream.WriteValue(layer, serializer));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Unit

        public override int CoefficientCount()
        {
            int count = 0;
            ForEach(layer => count += layer.CoefficientCount());
            return count;
        }

        public override int SetCoefficients(Single1D coefficients, int index)
        {
            ForEach(layer => index = layer.SetCoefficients(coefficients, index));
            return index;
        }

        public override int GetCoefficients(Single1D coefficients, int index)
        {
            ForEach(layer => index = layer.GetCoefficients(coefficients, index));
            return index;
        }

        public override int AddDerivatives(Single1D derivatives, int index, float lambdaDivN)
        {
            ForEach(layer => index = layer.AddDerivatives(derivatives, index, lambdaDivN));
            return index;
        }

        public override void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
            ForEach(layer => layer.Randomize(random, biasMagnitude, weightMagnitude));
        }

        public override int CountWeight()
        {
            int count = 0;
            ForEach(layer => count += layer.CountWeight());
            return count;
        }

        public override float SumWeightSqr()
        {
            float sum = 0f;
            ForEach(layer => sum += layer.SumWeightSqr());
            return sum;
        }

        public override int SetActivations(Single1D activations, int index)
        {
            ForEach(layer => index = layer.SetActivations(activations, index));
            return index;
        }

        public override int GetActivations(Single1D activations, int index)
        {
            ForEach(layer => index = layer.GetActivations(activations, index));
            return index;
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Layer

        public override void FeedForward(bool parallel)
        {
            ForEach(layer => layer.FeedForward(parallel));
        }

        public override void CalculateDeltas(Single1D ys, CostFunctionEnum costFunction)
        // This must be the last layer in the network
        {
        }

        public override void FeedBackward(bool parallel)
        {
            Layer layer = Last;
            while (layer != null)
            {
                layer.FeedBackward(parallel);
                layer = layer.Previous;
            }
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Network

        public float GetCostAndDerivatives(
            SampleList samples, Single1D derivatives, MeasurementList measurements,
            CalculationArguments arguments)
        {
            CostFunctionEnum costFunction = arguments.settings.CostFunction;
            float lambda = arguments.settings.Lambda;
            int nSamples = samples.Count;
            int nCoeffs = derivatives.Count;
            float cost = 0f;
            Layer last = Last;
            for (int i = 0; i < nCoeffs; i++) derivatives[i] = 0f;
            for (int iSample = 0; iSample < nSamples; iSample++)
            {
                arguments.ThrowIfCancellationRequested();
                Sample sample = samples[iSample];
                Single1D measurement = measurements[iSample];
                Previous.SetActivations(sample.Inputs, 0);
                FeedForward(true);
                last.GetActivations(measurement, 0);
                cost += CostFunction(measurement, sample.Requirements, costFunction);
                int weightCount = CountWeight();
                cost += 0.5f * lambda * SumWeightSqr() / weightCount; // regularization
                last.CalculateDeltas(sample.Requirements, costFunction);
                FeedBackward(true);
                AddDerivatives(derivatives, 0, lambda / weightCount);
                arguments.reporter?.ReportProgress(iSample, nSamples);
            }
            arguments.reporter?.ReportProgress(0, nSamples);
            cost /= nSamples;
            for (int i = 0; i < nCoeffs; i++) derivatives[i] /= nSamples;
            return cost;
        }

        public void Learn(
            SampleList samples, CalculationArguments arguments)
        // samples = yjks
        {
            arguments.reporter?.WriteStart($"Learning the network using a subset of {samples.Count} random samples...");
            Stopwatch timer = new Stopwatch();
            timer.Start();

            int nSamples = samples.Count; // number of sample rows
            int nCoefficients = CoefficientCount();
            // Current biasses and weights of the neurons in this network:
            Single1D coefficients = new Single1D(nCoefficients);
            // The derivatives of the cost with respect to the biasses and weights:
            Single1D derivatives = new Single1D(nCoefficients);
            Single1D velocities = new Single1D(nCoefficients);
            velocities.Clear();
            MeasurementList measurements = new MeasurementList(nSamples, OutputLayer.Count);
            GetCoefficients(coefficients, 0);
            Minimization minimization = new Minimization()
            {
                MaxIter = arguments.settings.MaxIter,
                Eps = arguments.settings.Epsilon,
                Tol = arguments.settings.Tolerance,
            };
            float finalCost = minimization.MomentumBasedGradientDescent(coefficients, derivatives, velocities,
                (iter) =>
                {
                    SetCoefficients(coefficients, 0);
                    arguments.reporter?.ReportCoefficients(coefficients);
                    float cost = GetCostAndDerivatives(samples, derivatives, measurements, arguments);
                    arguments.reporter?.ReportCostAndDerivatives(cost, derivatives, measurements);
                    return cost;
                }, arguments.settings.LearningRate, arguments.settings.MomentumCoefficient);
            arguments.reporter?.WriteEnd($"The network has learned in {timer.Elapsed.TotalSeconds} s, and the final cost value is {finalCost:F4}.");
        }


        private static float QuadraticCostFunction(Single1D aa, Single1D yy)
        {
            float cost = 0;
            int n = aa.Count;
            if (n != yy.Count) throw new UnequalValueException(n, yy.Count, 109047);
            for (int i = 0; i < n; i++)
            {
                cost += Sqr(aa[i] - yy[i]);
            }
            return cost / (2f * n);
        }

        private static float CrossEntropyCostFunction(Single1D aa, Single1D yy)
        {
            double cost = 0;
            int n = aa.Count;
            if (n != yy.Count) throw new UnequalValueException(n, yy.Count, 109047);
            for (int i = 0; i < n; i++)
            {
                double a = aa[i];
                double y = yy[i];
                cost += y * Log(a) + (1 - y) * Log(1 - a);
            }
            return -(float)cost / n;
        }

        private static float CostFunction(Single1D aa, Single1D yy, CostFunctionEnum costFunction)
        {
            return costFunction switch
            {
                CostFunctionEnum.Quadratic => QuadraticCostFunction(aa, yy),
                CostFunctionEnum.CrossEntropy => CrossEntropyCostFunction(aa, yy),
                _ => throw new InvalidCaseException(nameof(costFunction), costFunction, 386203),
            };
        }


        #endregion
    }
}
