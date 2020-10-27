using System;
using System.Collections.Generic;
using System.Drawing;
using Neulib.Numerics;

namespace Neunet.Images.Charts3D
{
    public class WireframeNode
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public List<WireframeNode> Nodes { get; set; }

        public WireframeImage WireframeImage { get; private set; }

        public object Parent { get; set; }

        public WireframeNode ParentNode { get { return Parent as WireframeNode; } }

        public List<Wireframe> Frames { get; private set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public WireframeNode(WireframeImage wireframeImage)
        {
            WireframeImage = wireframeImage;
            Frames = new List<Wireframe>();
            Nodes = new List<WireframeNode>();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseList

        public bool HasData()
        {
            if (Frames.Count > 0) return true;
            foreach (WireframeNode node in Nodes)
            {
                if (node.HasData()) return true;
            }
            return false;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region WireframeList

        public void CalculatePointsF(float length, Func<Single3, PointF> func)
        {
            foreach (Wireframe frame in Frames)
                frame.CalculatePointsF(length, func);
            foreach (WireframeNode node in Nodes)
                node.CalculatePointsF(length, func);
        }

        public void CalculateColors(float min, float max)
        {
            foreach (Wireframe frame in Frames)
                frame.CalculateColors(min, max);
            foreach (WireframeNode node in Nodes)
                node.CalculateColors(min, max);
        }

        /// <summary>
        /// Calculates and updates wireframe properties from the current wireframe data.
        /// </summary>
        public void UpdateDimensions(WireframeDimensions dimensions)
        {
            foreach (Wireframe frame in Frames)
                frame.UpdateDimensions(dimensions);
            foreach (WireframeNode node in Nodes)
                node.UpdateDimensions(dimensions);
        }

        public void DrawWireframes(Graphics graphics, WireframeImage wireframeImage)
        {

                foreach (Wireframe frame in Frames)
                {
                    frame.DrawFrame(graphics);
                }
                foreach (WireframeNode node in Nodes)
                {
                    node.DrawWireframes(graphics, wireframeImage);
                }
        }

        public WireframeNode AddNewChildNode()
        {
            WireframeNode childNode = new WireframeNode(WireframeImage)
            {
                Parent = this
            };
            Nodes.Add(childNode);
            return childNode;
        }

        public Wireframe AddNewWireframe(int hCount, int uCount, int vCount, bool uPeriodic, bool vPeriodic)
        {
            Wireframe wireframe = new Wireframe(hCount, uCount, vCount)
            {
                ParentNode = this,
                UPeriodic = uPeriodic,
                VPeriodic = vPeriodic,
            };
            Frames.Add(wireframe);
            return wireframe;
        }

        #endregion
    }
}
