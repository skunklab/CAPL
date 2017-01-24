﻿/*
Claims Authorization Policy Langugage SDK ver. 1.0
 
Copyright (c) Matt Long labskunk@gmail.com
 
All rights reserved.
 
MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the ""Software""), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace Capl.Authorization
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using Capl.Authorization.Transforms;

    /// <summary>
    /// The base class for an authorization policy.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("GetSchema", IsAny = false)]
    [KnownType(typeof(AuthorizationPolicy))]
    public abstract class AuthorizationPolicyBase : IXmlSerializable
    {
        /// <summary>
        /// Gets or sets a transform collection.
        /// </summary>
        public abstract TransformCollection Transforms { get; internal set; }

        /// <summary>
        /// Gets or sets an evaluation expression.
        /// </summary>
        public abstract Term Expression { get; set; }

        /// <summary>
        /// Provides a schema for an authorization policy.
        /// </summary>
        /// <param name="schemaSet">A schema set to populate.</param>
        /// <returns>Qualified name of an authorization policy type for a schema.</returns>
        public static XmlQualifiedName GetSchema(XmlSchemaSet schemaSet)
        {
            if (schemaSet == null)
            {
                throw new ArgumentNullException("schemaSet");
            }

            using (StringReader reader = new StringReader(Properties.Resources.AuthorizationPolicySchema))
            {
                XmlSchema schema = XmlSchema.Read(reader, null);
                schemaSet.Add(schema);
            }

            return new XmlQualifiedName("AuthorizationPolicyType", AuthorizationConstants.Namespaces.Xmlns);
        }

        #region IXmlSerializable Members

        /// <summary>
        /// Provides a schema for an authorization policy.
        /// </summary>
        /// <returns>Schema for an authorization policy.</returns>
        /// <remarks>The methods always return null; the schema is provided by an XmlSchemaProvider.</remarks>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Reads the Xml of an authorization policy.
        /// </summary>
        /// <param name="reader">An XmlReader for the authorization policy.</param>
        public abstract void ReadXml(XmlReader reader);

        /// <summary>
        /// Writes the Xml of an authorization policy.
        /// </summary>
        /// <param name="writer">An XmlWriter for the authorization policy.</param>
        public abstract void WriteXml(XmlWriter writer);

        #endregion        
    }
}