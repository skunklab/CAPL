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

namespace Capl.ServiceModel
{
    using Capl.Authorization;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    public class BlobStore : ICaplStore
    {
        private static BlobStore instance;

        public static BlobStore Create(string container, string connectionString)
        {
            if(instance == null)
            {
                instance = new BlobStore(container, connectionString);
            }

            return instance;
        }
        protected BlobStore(string container, string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            client = storageAccount.CreateCloudBlobClient();
            blobContainer = client.GetContainerReference(container);

            Task task = Task.Factory.StartNew(async () =>
            {
                await blobContainer.CreateIfNotExistsAsync();
            });

            Task.WhenAll(task);
        }

   
        private CloudBlobClient client;
        private CloudBlobContainer blobContainer;

        public AuthorizationPolicy GetPolicy(string policyId)
        {
            AuthorizationPolicy policy = null;
            Uri policyUri = new Uri(policyId);
            string policyUriString = policyUri.ToString().ToLower(CultureInfo.InvariantCulture);

            string filename = GetFilename(policyUriString);
           
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(filename);
            using (MemoryStream stream = new MemoryStream())
            {
                blockBlob.DownloadToStream(stream);
                stream.Position = 0;

                using(XmlReader reader = XmlReader.Create(stream))
                {
                    policy = AuthorizationPolicy.Load(reader);
                    reader.Close();
                }

                stream.Close();
            }

            return policy;           
        }

        public void SetPolicy(AuthorizationPolicy policy)
        {
            byte[] blobBytes = null;
            XmlWriterSettings settings = new XmlWriterSettings() { OmitXmlDeclaration = true };
            using(MemoryStream stream = new MemoryStream())
            {
                using(XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    policy.WriteXml(writer);
                    writer.Flush();
                    writer.Close();
                    stream.Position = 0;
                    blobBytes = stream.ToArray();
                }
                stream.Close();
            }

            string policyUriString = policy.PolicyId.ToString().ToLower(CultureInfo.InvariantCulture);


            string filename = GetFilename(policyUriString);

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(filename);            
            blockBlob.Properties.ContentType = "application/xml";

            Task task = blockBlob.UploadFromByteArrayAsync(blobBytes, 0, blobBytes.Length); 
            Task.WhenAll(task);
        }

        private string GetFilename(string policyUriString)
        {
            byte[] hash = null;
            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(policyUriString));
            }

            return String.Format("{0}.xml", new Guid(hash).ToString());
        }




    }
}
