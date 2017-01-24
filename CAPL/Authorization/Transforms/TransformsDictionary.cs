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

namespace Capl.Authorization.Transforms
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A dictionary that contains transforms that can be identified by their respective URIs for their operations.
    /// </summary>
    public class TransformsDictionary : IDictionary<string, TransformAction>
    {
        /// <summary>
        /// Creates an instance of the object.
        /// </summary>
        public TransformsDictionary()
        {
            transforms = new Dictionary<string, TransformAction>();
        }

        Dictionary<string, TransformAction> transforms;

        #region IDictionary<string,TransformAction> Members

        /// <summary>
        /// Adds a new transform.
        /// </summary>
        /// <param name="key">The key that identifies the tranform.</param>
        /// <param name="value">The transform instance.</param>
        public void Add(string key, TransformAction value)
        {
            transforms.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return transforms.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return transforms.Keys; }
        }

        public bool Remove(string key)
        {
            return transforms.Remove(key);
        }

        public bool TryGetValue(string key, out TransformAction value)
        {
            return transforms.TryGetValue(key, out value);
        }

        public ICollection<TransformAction> Values
        {
            get { return transforms.Values; }
        }

        public TransformAction this[string key]
        {
            get { return transforms[key]; }
            set { transforms[key] = value; }
        }

        #endregion

        #region ICollection<KeyValuePair<string,TransformAction>> Members

        public void Add(KeyValuePair<string, TransformAction> item)
        {
            ((ICollection<KeyValuePair<string, TransformAction>>)transforms).Add(item);
        }

        public void Clear()
        {
            transforms.Clear();
        }

        public bool Contains(KeyValuePair<string, TransformAction> item)
        {
            return ((ICollection<KeyValuePair<string, TransformAction>>)transforms).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, TransformAction>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, TransformAction>>)transforms).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return transforms.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, TransformAction> item)
        {
            return ((ICollection<KeyValuePair<string, TransformAction>>)transforms).Remove(item);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,TransformAction>> Members

        public IEnumerator<KeyValuePair<string, TransformAction>> GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<string, TransformAction>>)transforms.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return transforms.GetEnumerator();
        }

        #endregion
    }
}