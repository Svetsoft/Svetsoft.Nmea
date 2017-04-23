using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents which devices are used to obtain a fix, other than the GPS device itself.
    /// </summary>
    public struct FixQuality
    {
        private static readonly IList<FixQuality> InternalList = new[]
        {
            BeaconDifferentialGpsFix,
            DifferentialGpsFix,
            Estimated,
            FixedRealTimeKinematic,
            FloatRealTimeKinematic,
            GpsFix,
            HpXpOmniStar,
            LocationRealTimeKinematic,
            ManualInput,
            Network2DFixedRealTimeKinematic,
            Network2DFloatRealTimeKinematic,
            Network3DFixedRealTimeKinematic,
            Network3DFloatRealTimeKinematic,
            NoFix,
            PulsePerSecond,
            SatelliteBasedAugmentationSystem,
            Simulated,
            Unknown,
            VbsOmniStar
        };

        /// <summary>
        ///     Converts a string to its <see cref="FixQuality" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="FixQuality" /> equivalent of the string.</returns>
        public static FixQuality Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException($"{nameof(value)} is not in the correct format");
            }

            var parsedValue = int.Parse(value);
            foreach (var item in InternalList)
            {
                if (parsedValue == item.Value)
                {
                    return item;
                }
            }

            throw new FormatException($"{nameof(value)} is not in the correct format");
        }

        /// <summary>
        ///     Returns a read-only list of supported devices to obtain a fix.
        /// </summary>
        public ReadOnlyCollection<FixQuality> List
        {
            get { return new ReadOnlyCollection<FixQuality>(InternalList); }
        }

        /// <summary>
        ///     Returns the value that this fix quality represents.
        /// </summary>
        internal int Value { get; }

        /// <summary>
        ///     Returns the name of the device used to obtain the fix.
        /// </summary>
        internal string Name { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FixQuality" /> class.
        /// </summary>
        /// <param name="name">The name of the device used to obtain the fix.</param>
        /// <param name="value">The value that the fix quality represents.</param>
        internal FixQuality(string name, int value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        ///     Represents that not enough information is available to specify the current fix quality. This field is read-only.
        /// </summary>
        public static readonly FixQuality Unknown = new FixQuality("Unknown", 0);

        /// <summary>
        ///     Represents that no fix is obtained. This field is read-only.
        /// </summary>
        public static readonly FixQuality NoFix = new FixQuality("NoFix", 1);

        /// <summary>
        ///     Represents a fix obtained using GPS satellites only. This field is read-only.
        /// </summary>
        public static readonly FixQuality GpsFix = new FixQuality("GpsFix", 2);

        /// <summary>
        ///     Represents a fix obtained using both GPS satellites and Differential GPS/WAAS ground
        ///     stations. Position error is as low as 0.5-5 meters. This field is read-only.
        /// </summary>
        public static readonly FixQuality DifferentialGpsFix = new FixQuality("DifferentialGpsFix", 3);

        /// <summary>
        ///     Represents a PPS or pulse-per-second fix. This field is read-only.
        /// </summary>
        public static readonly FixQuality PulsePerSecond = new FixQuality("PulsePerSecond", 4);

        /// <summary>
        ///     Represents a fix obtained with the assistance of a reference station. Position error is as low as 1-5 centimeters.
        ///     This field is read-only.
        /// </summary>
        public static readonly FixQuality FixedRealTimeKinematic = new FixQuality("FixedRealTimeKinematic", 5);

        /// <summary>
        ///     Represents a fix obtained with the assistance of a reference station. Position error is as low as 20cm to 1 meter.
        ///     This field is read-only.
        /// </summary>
        public static readonly FixQuality FloatRealTimeKinematic = new FixQuality("FloatRealTimeKinematic", 6);

        /// <summary>
        ///     Represents an estimated fix. This field is read-only.
        /// </summary>
        public static readonly FixQuality Estimated = new FixQuality("Estimated", 7);

        /// <summary>
        ///     Represents a fix provided manually. This field is read-only.
        /// </summary>
        public static readonly FixQuality ManualInput = new FixQuality("ManualInput", 8);

        /// <summary>
        ///     Represents a simulated fix. This field is read-only.
        /// </summary>
        public static readonly FixQuality Simulated = new FixQuality("Simulated", 9);

        /// <summary>
        ///     Represents a fix based on Differential GPS but applies to wide area (WAAS/EGNOS and MSAS). This field is read-only.
        /// </summary>
        public static readonly FixQuality SatelliteBasedAugmentationSystem = new FixQuality("SatelliteBasedAugmentationSystem", 10);

        /// <summary>
        ///     Represents a RTK float or Location RTK mode 3D Network. This field is read-only.
        /// </summary>
        public static readonly FixQuality Network3DFloatRealTimeKinematic = new FixQuality("Network3DFloatRealTimeKinematic", 11);

        /// <summary>
        ///     Represents a RTK fixed 3D Network. This field is read-only.
        /// </summary>
        public static readonly FixQuality Network3DFixedRealTimeKinematic = new FixQuality("Network3DFixedRealTimeKinematic", 12);

        /// <summary>
        ///     Represents a RTK float or Location RTK mode 2D Network. This field is read-only.
        /// </summary>
        public static readonly FixQuality Network2DFloatRealTimeKinematic = new FixQuality("Network2DFloatRealTimeKinematic", 13);

        /// <summary>
        ///     Represents a RTK fixed 2D Network. This field is read-only.
        /// </summary>
        public static readonly FixQuality Network2DFixedRealTimeKinematic = new FixQuality("Network2DFixedRealTimeKinematic", 14);

        /// <summary>
        ///     Represents a fix that utilizes a global satellite monitoring network. Omnistar with XP is accurate in 3D to better
        ///     than 30cm. This field is read-only.
        /// </summary>
        public static readonly FixQuality HpXpOmniStar = new FixQuality("HpXpOmniStar", 15);

        /// <summary>
        ///     Represents a fix obtained using "sub-meter" level of service. This field is read-only.
        /// </summary>
        public static readonly FixQuality VbsOmniStar = new FixQuality("VbsOmniStar", 16);

        /// <summary>
        ///     Represents a fix is obtained using Location RTK mode with horizontal accuracy of 10cm and vertical accuracy of 2cm.
        ///     This field is read-only.
        /// </summary>
        public static readonly FixQuality LocationRealTimeKinematic = new FixQuality("LocationRealTimeKinematic", 17);

        /// <summary>
        ///     Represents a beacon Differential GPS. This field is read-only.
        /// </summary>
        public static readonly FixQuality BeaconDifferentialGpsFix = new FixQuality("BeaconDifferentialGpsFix", 18);
    }
}