using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static System.Convert;
using static System.Math;

namespace Neunet.Images.Charts3D
{
    public partial class WireframeImage : Chart3DImage
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private WireframeDimensions _dimensions;

        private WireframeNode _wireframeNode;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public WireframeNode WireframeNode
        {
            get { return _wireframeNode; }
            set
            {
                if (Equals(value, _wireframeNode))
                    return;
                SetWireframeNode(value);
            }
        }

        private void SetWireframeNode(WireframeNode value)
        {
            _wireframeNode = value;
            try
            {
                SuspendImage();
                UpdateDimensions();
            }
            finally
            {
                ResumeImage();
            }
        }

        private const int _deltaLevelDefault = 0;
        private int _deltaLevel = _deltaLevelDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int DeltaLevel
        {
            get { return _deltaLevel; }
            set
            {
                if (value == _deltaLevel) return;
                _deltaLevel = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region UV-Index

        private const int _uIndexDefault = 0;
        private int _uIndex = _uIndexDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int UIndex
        {
            get { return _uIndex; }
            set
            {
                if (value == _uIndex) return;
                _uIndex = value;
                RefreshImage();
            }
        }

        private const int _vIndexDefault = 0;
        private int _vIndex = _vIndexDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int VIndex
        {
            get { return _vIndex; }
            set
            {
                if (value == _vIndex) return;
                _vIndex = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SliceMode

        public const SliceModeEnum sliceModeDefault = SliceModeEnum.None;
        private SliceModeEnum _sliceMode = sliceModeDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public SliceModeEnum SliceMode
        {
            get { return _sliceMode; }
            set
            {
                if (value == _sliceMode) return;
                _sliceMode = value;
                try
                {
                    SuspendImage();
                    SliceValue = 0;
                }
                finally
                {
                    ResumeImage();
                }
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SliceValue

        public const int sliceValueDefault = 0;
        private int _sliceValue = sliceValueDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int SliceValue
        {
            get { return _sliceValue; }
            set
            {
                if (value == _sliceValue) return;
                _sliceValue = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ColorGradient

        private const ColorGradientEnum _colorGradientDefault = ColorGradientEnum.Gradient5;
        private ColorGradientEnum _colorGradient = _colorGradientDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public ColorGradientEnum ColorGradient
        {
            get { return _colorGradient; }
            set
            {
                if (value == ColorGradient) return;
                _colorGradient = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ViewRays

        public const bool viewRaysDefault = false;
        private bool _viewRays = viewRaysDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewRays
        {
            get { return _viewRays; }
            set
            {
                if (value == ViewRays) return;
                _viewRays = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ViewNormalVectors

        public const bool viewNormalsDefault = false;
        private bool _viewNormals = viewNormalsDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewNormals
        {
            get { return _viewNormals; }
            set
            {
                if (value == ViewNormals) return;
                _viewNormals = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region InverseGradient

        private const bool _inverseGradientDefault = false;
        private bool _inverseGradient = _inverseGradientDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool InverseGradient
        {
            get { return _inverseGradient; }
            set
            {
                if (value == InverseGradient) return;
                _inverseGradient = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ViewUVAxes

        private const bool _viewUVAxesDefault = true;
        private bool _viewUVAxes = _viewUVAxesDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewUVAxes
        {
            get { return _viewUVAxes; }
            set
            {
                if (value == ViewUVAxes) return;
                _viewUVAxes = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ViewSurfaces

        private const bool _viewSurfacesDefault = true;
        private bool _viewSurfaces = _viewSurfacesDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ViewSurfaces
        {
            get { return _viewSurfaces; }
            set
            {
                if (value == ViewSurfaces) return;
                _viewSurfaces = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region RayPenWidth

        private const float _rayPenWidthDefault = 1f;
        private float _rayPenWidth = _rayPenWidthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float RayPenWidth
        {
            get { return _rayPenWidth; }
            set
            {
                if (value == RayPenWidth) return;
                _rayPenWidth = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region RayCapSize

        private const float _rayCapSizeDefault = 3f;
        private float _rayCapSize = _rayCapSizeDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float RayCapSize
        {
            get { return _rayCapSize; }
            set
            {
                if (value == RayCapSize) return;
                _rayCapSize = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SurfacePenWidth

        private const float _surfacePenWidthDefault = 1f;
        private float _surfacePenWidth = _surfacePenWidthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float SurfacePenWidth
        {
            get { return _surfacePenWidth; }
            set
            {
                if (value == SurfacePenWidth) return;
                _surfacePenWidth = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region UVAxisPenWidth

        private const float _uvAxisPenWidthDefault = 2f;
        private float _uvAxisPenWidth = _uvAxisPenWidthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float UVAxisPenWidth
        {
            get { return _uvAxisPenWidth; }
            set
            {
                if (value == UVAxisPenWidth) return;
                _uvAxisPenWidth = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region UVAxisCapSize

        private const float _uvAxisCapSizeDefault = 5f;
        private float _uvAxisCapSize = _uvAxisCapSizeDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float UVAxisCapSize
        {
            get { return _uvAxisCapSize; }
            set
            {
                if (value == UVAxisCapSize) return;
                _uvAxisCapSize = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region NormalVectorPenWidth

        private const float _normalVectorPenWidthDefault = 1f;
        private float _normalVectorPenWidth = _normalVectorPenWidthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float NormalVectorPenWidth
        {
            get { return _normalVectorPenWidth; }
            set
            {
                if (value == NormalVectorPenWidth) return;
                _normalVectorPenWidth = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region VectorCapSize

        private const float _vectorCapSizeDefault = 3f;
        private float _vectorCapSize = _vectorCapSizeDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float VectorCapSize
        {
            get { return _vectorCapSize; }
            set
            {
                if (value == VectorCapSize) return;
                _vectorCapSize = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region VectorLength

        private const float _vectorLengthDefault = 1f;
        private float _vectorLength = _vectorLengthDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public float VectorLength
        {
            get { return _vectorLength; }
            set
            {
                if (value == VectorLength) return;
                _vectorLength = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SurfaceColor

        private static readonly Color _surfaceColorDefault = Color.DarkBlue;
        private Color _SurfaceColor = _surfaceColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color SurfaceColor
        {
            get { return _SurfaceColor; }
            set
            {
                if (value == SurfaceColor) return;
                _SurfaceColor = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region InvalidColor

        private static readonly Color _ValueNaNColorDefault = Color.Gray;
        private Color _ValueNaNColor = _ValueNaNColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color InvalidColor
        {
            get { return _ValueNaNColor; }
            set
            {
                if (value == InvalidColor) return;
                _ValueNaNColor = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region UAxisColor

        private static readonly Color _uAxisColorDefault = Color.Red;
        private Color _uAxisColor = _uAxisColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color UAxisColor
        {
            get { return _uAxisColor; }
            set
            {
                if (value == UAxisColor) return;
                _uAxisColor = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region VAxisColor

        private static readonly Color _vAxisColorDefault = Color.Blue;
        private Color _vAxisColor = _vAxisColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color VAxisColor
        {
            get { return _vAxisColor; }
            set
            {
                if (value == VAxisColor) return;
                _vAxisColor = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region RayColor

        private static readonly Color _RefractionColorDefault = Color.DarkOrange;
        private Color _RefractionColor = _RefractionColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color RefractionColor
        {
            get { return _RefractionColor; }
            set
            {
                if (value == _RefractionColor) return;
                _RefractionColor = value;
                RefreshImage();
            }
        }

        private static readonly Color _ReflectionColorDefault = Color.DarkMagenta;
        private Color _ReflectionColor = _ReflectionColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color ReflectionColor
        {
            get { return _ReflectionColor; }
            set
            {
                if (value == _ReflectionColor) return;
                _ReflectionColor = value;
                RefreshImage();
            }
        }


        private static readonly Color _TirColorDefault = Color.DarkCyan;
        private Color _TirColor = _TirColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color TirColor
        {
            get { return _TirColor; }
            set
            {
                if (value == _TirColor) return;
                _TirColor = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region NormalsColor

        private static readonly Color _NormalsColorDefault = Color.Blue;
        private Color _NormalsColor = _NormalsColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color NormalsColor
        {
            get { return _NormalsColor; }
            set
            {
                if (value == NormalsColor) return;
                _NormalsColor = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region PointsFillColor

        private static readonly Color _pointsFillColorDefault = Color.LightBlue;
        private Color _pointsFillColor = _pointsFillColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color PointsFillColor
        {
            get { return _pointsFillColor; }
            set
            {
                if (value == _pointsFillColor) return;
                _pointsFillColor = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region PointsOutlineColor

        private static readonly Color _pointsOutlineColorDefault = Color.DarkBlue;
        private Color _pointsOutlineColor = _pointsOutlineColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color PointsOutlineColor
        {
            get { return _pointsOutlineColor; }
            set
            {
                if (value == _pointsOutlineColor) return;
                _pointsOutlineColor = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region PointsCrossColor

        private static readonly Color _pointsCrossColorDefault = Color.DarkRed;
        private Color _pointsCrossColor = _pointsCrossColorDefault;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color PointsCrossColor
        {
            get { return _pointsCrossColor; }
            set
            {
                if (value == _pointsCrossColor) return;
                _pointsCrossColor = value;
                RefreshImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public WireframeImage()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Cleans up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseImage

        private float minValue = float.NaN, maxValue = float.NaN;

        public override void DrawImage(Bitmap bitmap)
        {
            if (WireframeNode == null) return;
            base.DrawImage(bitmap);
            WireframeNode.CalculatePointsF(VectorLength * UnitVectorLength, point => Project(point));
            using (Graphics graphics = Graphics.FromImage(bitmap)) 
                WireframeNode.DrawWireframes(graphics, this);
            GradientBitmap.DrawLegend(bitmap, minValue, maxValue, "G4", ColorGradient, InverseGradient, null);
        }

        protected override void ImageMouseDown(int x0, int y0, MouseButtons buttons)
        {
            base.ImageMouseDown(x0, y0, buttons);
            //_mouseDown = true;
        }

        protected override void ImageMouseUp(int x0, int y0, int dx, int dy, MouseButtons buttons)
        {
            base.ImageMouseUp(x0, y0, dx, dy, buttons);
            //_mouseDown = false;
            RefreshImage();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region View3DImage

        public void UpdateDimensions()
        {
            WireframeDimensions dimensions = new WireframeDimensions();
            WireframeNode?.UpdateDimensions(dimensions);
            _dimensions = dimensions;
        }

        public override float BiggestSize
        {
            get
            {
                float biggestSize = 1f; // default
                if (_dimensions != null && !float.IsNaN(_dimensions.BiggestSize) && _dimensions.BiggestSize > 0f)
                    biggestSize = _dimensions.BiggestSize;
                return biggestSize;
            }
        }

        public override void ResetOrigin()
        {
            if (_dimensions == null) return;
            Origin = _dimensions.Center;
        }

        public override void ResetZoom(float fit = 0.9f)
        {
            float a = Min(pictureBox.Width, pictureBox.Height);
            Zoom = fit * ToSingle(a) / ToSingle(BiggestSize);
        }

        public void ResetOriginAndZoom()
        {
            try
            {
                SuspendImage();
                ResetOrigin();
                ResetZoom();
            }
            finally
            {
                ResumeImage();
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region WireframeImage

        public WireframeNode CreateRootNode()
        {
            WireframeNode childNode = new WireframeNode(this);
            return childNode;
        }

        public void ResetResolution()
        {
            DeltaLevel = 0;
        }

        public void DecreaseResolution()
        {
            DeltaLevel--;
        }

        public void IncreaseResolution()
        {
            DeltaLevel++;
        }

        #endregion
    }
}

