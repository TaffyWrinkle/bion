// Copyright (c) Microsoft.  All Rights Reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BSOA.Model;

using Microsoft.CodeAnalysis.Sarif;
using Microsoft.CodeAnalysis.Sarif.Readers;

using Newtonsoft.Json;

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.CodeAnalysis.Sarif
{
    /// <summary>
    ///  GENERATED: BSOA Entity for 'Fix'
    /// </summary>
    [DataContract]
    [GeneratedCode("BSOA.Generator", "0.5.0")]
    public partial class Fix : PropertyBagHolder, ISarifNode, IRow
    {
        private FixTable _table;
        private int _index;

        public Fix() : this(SarifLogDatabase.Current.Fix)
        { }

        public Fix(SarifLog root) : this(root.Database.Fix)
        { }

        internal Fix(FixTable table) : this(table, table.Count)
        {
            table.Add();
        }

        internal Fix(FixTable table, int index)
        {
            this._table = table;
            this._index = index;
        }

        public Fix(
            Message description,
            IList<ArtifactChange> artifactChanges,
            IDictionary<string, string> properties
        ) 
            : this(SarifLogDatabase.Current.Fix)
        {
            Description = description;
            ArtifactChanges = artifactChanges;
            Properties = properties;
        }

        public Fix(Fix other) 
            : this(SarifLogDatabase.Current.Fix)
        {
            Description = other.Description;
            ArtifactChanges = other.ArtifactChanges;
            Properties = other.Properties;
        }

        [DataMember(Name = "description", IsRequired = false, EmitDefaultValue = false)]
        public Message Description
        {
            get => _table.Database.Message.Get(_table.Description[_index]);
            set => _table.Description[_index] = _table.Database.Message.LocalIndex(value);
        }

        [DataMember(Name = "artifactChanges", IsRequired = false, EmitDefaultValue = false)]
        public IList<ArtifactChange> ArtifactChanges
        {
            get => _table.Database.ArtifactChange.List(_table.ArtifactChanges[_index]);
            set => _table.Database.ArtifactChange.List(_table.ArtifactChanges[_index]).SetTo(value);
        }

        [DataMember(Name = "properties", IsRequired = false, EmitDefaultValue = false)]
        internal override IDictionary<string, string> Properties
        {
            get => _table.Properties[_index];
            set => _table.Properties[_index] = value;
        }

        #region IEquatable<Fix>
        public bool Equals(Fix other)
        {
            if (other == null) { return false; }

            if (this.Description != other.Description) { return false; }
            if (this.ArtifactChanges != other.ArtifactChanges) { return false; }
            if (this.Properties != other.Properties) { return false; }

            return true;
        }
        #endregion

        #region Object overrides
        public override int GetHashCode()
        {
            int result = 17;

            unchecked
            {
                if (Description != default(Message))
                {
                    result = (result * 31) + Description.GetHashCode();
                }

                if (ArtifactChanges != default(IList<ArtifactChange>))
                {
                    result = (result * 31) + ArtifactChanges.GetHashCode();
                }

                if (Properties != default(IDictionary<string, string>))
                {
                    result = (result * 31) + Properties.GetHashCode();
                }
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Fix);
        }

        public static bool operator ==(Fix left, Fix right)
        {
            if (object.ReferenceEquals(left, null))
            {
                return object.ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Fix left, Fix right)
        {
            if (object.ReferenceEquals(left, null))
            {
                return !object.ReferenceEquals(right, null);
            }

            return !left.Equals(right);
        }
        #endregion

        #region IRow
        ITable IRow.Table => _table;
        int IRow.Index => _index;

        void IRow.Reset(ITable table, int index)
        {
            _table = (FixTable)table;
            _index = index;
        }
        #endregion

        #region ISarifNode
        public SarifNodeKind SarifNodeKind => SarifNodeKind.Fix;

        ISarifNode ISarifNode.DeepClone()
        {
            return DeepCloneCore();
        }

        /// <summary>
        /// Creates a deep copy of this instance.
        /// </summary>
        public Fix DeepClone()
        {
            return (Fix)DeepCloneCore();
        }

        private ISarifNode DeepCloneCore()
        {
            return new Fix(this);
        }
        #endregion

        public static IEqualityComparer<Fix> ValueComparer => EqualityComparer<Fix>.Default;
        public bool ValueEquals(Fix other) => Equals(other);
        public int ValueGetHashCode() => GetHashCode();
    }
}
