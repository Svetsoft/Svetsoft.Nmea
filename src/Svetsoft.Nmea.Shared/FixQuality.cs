namespace Svetsoft.Geography.Positioning
{
    /// <summary>
    ///     Indicates which devices are being used to obtain a fix, other than the GPS device itself.
    /// </summary>
    public enum FixQuality
    {
        /// <summary>
        ///     Not enough information is available to specify the current fix quality.
        /// </summary>
        Unknown,

        /// <summary>
        ///     No fix is obtained.
        /// </summary>
        NoFix,

        /// <summary>
        ///     Fix is currently obtained using GPS satellites only.
        /// </summary>
        GpsFix,

        /// <summary>
        ///     Fix is obtained using both GPS satellites and Differential GPS/WAAS ground
        ///     stations. Position error is as low as 0.5-5 meters.
        /// </summary>
        DifferentialGpsFix,

        /// <summary>
        ///     A PPS or pulse-per-second fix.
        /// </summary>
        PulsePerSecond,

        /// <summary>
        ///     Fix is obtained with the assistance of a reference station. Position error is as low as 1-5 centimeters.
        /// </summary>
        FixedRealTimeKinematic,

        /// <summary>
        ///     Fix is obtained with the assistance of a reference station. Position error is as low as 20cm to 1 meter.
        /// </summary>
        FloatRealTimeKinematic,

        /// <summary>
        ///     The fix is estimated.
        /// </summary>
        Estimated,

        /// <summary>
        ///     The fix is provided manually.
        /// </summary>
        ManualInput,

        /// <summary>
        ///     The fix is simulated.
        /// </summary>
        Simulated,

        /// <summary>
        ///     The fix is based on Differential GPS but applies to wide area (WAAS/EGNOS and MSAS).
        /// </summary>
        SatelliteBasedAugmentationSystem,

        /// <summary>
        ///     RTK float or Location RTK mode 3D Network.
        /// </summary>
        Network3DFloatRealTimeKinematic,

        /// <summary>
        ///     RTK fixed 3D Network.
        /// </summary>
        Network3DFixedRealTimeKinematic,

        /// <summary>
        ///     RTK float or Location RTK mode 2D Network.
        /// </summary>
        Network2DFloatRealTimeKinematic,

        /// <summary>
        ///     RTK fixed 2D Network.
        /// </summary>
        Network2DFixedRealTimeKinematic,

        /// <summary>
        ///     Fix utilizes a global satellite monitoring network. Omnistar with XP is accurate in 3D to better than 30cm.
        /// </summary>
        HpXpOmniStar,

        /// <summary>
        ///     Fix is obtained using "sub-meter" level of service.
        /// </summary>
        VbsOmniStar,

        /// <summary>
        ///     Fix is obtained using Location RTK mode with horizontal accuracy of 10cm and vertical accuracy of 2cm.
        /// </summary>
        LocationRealTimeKinematic,

        /// <summary>
        ///     Beacon Differential GPS.
        /// </summary>
        BeaconDifferentialGpsFix
    }
}