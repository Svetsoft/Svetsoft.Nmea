using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for geographic location in time differences.
    /// </summary>
    public class GtdSentence : NmeaSentence
    {
        private readonly List<string> _timeDifferences;

        /// <summary>
        ///     Creates a new instance of the <see cref="GtdSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GtdSentence(string sentence)
            : base(sentence)
        {
            _timeDifferences = new List<string>();
            Parse();
        }

        /// <summary>
        ///     Returns the list of time differences.
        /// </summary>
        public ReadOnlyCollection<string> TimeDifferences
        {
            get { return new ReadOnlyCollection<string>(_timeDifferences); }
        }

        /// <summary>
        ///     Adds <see cref="string" /> elements of the specified collection to the end of the list.
        /// </summary>
        /// <param name="collection">The collection whose <see cref="string" /> elements should be added to the end of the list.</param>
        internal void AddTimeDifferencesRange(IEnumerable<string> collection)
        {
            _timeDifferences.AddRange(collection);
        }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GtdSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            AddTimeDifferencesRange(GetString(0, 5));
        }
    }
}