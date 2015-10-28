using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.IO;
using System.IO.IsolatedStorage;
using DropIt.Data.Interfaces.Services;
using DropIt.Droid.Dependencies;
using Java.Lang;
using Java.Security;
using Javax.Crypto;
using Exception = System.Exception;
using Object = Java.Lang.Object;

[assembly: Xamarin.Forms.Dependency(typeof(KeyVaultStorage))]

namespace DropIt.Droid.Dependencies
{
    /// <summary>
    /// Implementation of <see cref="ISecureStorage"/> using Android KeyStore.
    /// </summary>
    public class KeyVaultStorage : ISecureStorage
    {
        private static IsolatedStorageFile File => IsolatedStorageFile.GetUserStoreForApplication();
        private static readonly object SaveLock = new object();

        private const string StorageFile = "XLabs.Platform.Services.KeyVaultStorage";

        private readonly KeyStore keyStore;
        private readonly KeyStore.PasswordProtection protection;

        public static string Password { get; set; }

        public KeyVaultStorage()
        {
            this.keyStore = KeyStore.GetInstance(KeyStore.DefaultType);
            this.protection = new KeyStore.PasswordProtection(Password.ToCharArray());

            if (File.FileExists(StorageFile))
            {
                using (var stream = new IsolatedStorageFileStream(StorageFile, FileMode.Open, FileAccess.Read, File))
                {
                    this.keyStore.Load(stream, Password.ToCharArray());
                }
            }
            else
            {
                this.keyStore.Load(null, Password.ToCharArray());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyVaultStorage"/> class.
        /// </summary>
        /// <param name="password">Password to use for encryption.</param>
        public KeyVaultStorage(char[] password)
        {
            this.keyStore = KeyStore.GetInstance(KeyStore.DefaultType);
            this.protection = new KeyStore.PasswordProtection(password);

            if (File.FileExists(StorageFile))
            {
                using (var stream = new IsolatedStorageFileStream(StorageFile, FileMode.Open, FileAccess.Read, File))
                {
                    this.keyStore.Load(stream, password);
                }
            }
            else
            {
                this.keyStore.Load(null, password);
            }
        }

        #region ISecureStorage Members

        /// <summary>
        /// Stores data.
        /// </summary>
        /// <param name="key">Key for the data.</param>
        /// <param name="dataBytes">Data bytes to store.</param>
        public void Store(string key, byte[] dataBytes)
        {
            this.keyStore.SetEntry(key, new KeyStore.SecretKeyEntry(new SecureData(dataBytes)), this.protection);
            Save();
        }

        /// <summary>
        /// Retrieves stored data.
        /// </summary>
        /// <param name="key">Key for the data.</param>
        /// <returns>Byte array of stored data.</returns>
        public byte[] Retrieve(string key)
        {
            var entry = this.keyStore.GetEntry(key, this.protection) as KeyStore.SecretKeyEntry;

            if (entry == null)
            {
                throw new Exception(string.Format("No entry found for key {0}.", key));
            }

            return entry.SecretKey.GetEncoded();
        }

        /// <summary>
        /// Deletes data.
        /// </summary>
        /// <param name="key">Key for the data to be deleted.</param>
        public void Delete(string key)
        {
            this.keyStore.DeleteEntry(key);
            Save();
        }

        /// <summary>
        /// Checks if the storage contains a key.
        /// </summary>
        /// <param name="key">The key to search.</param>
        /// <returns>True if the storage has the key, otherwise false.</returns>
        public bool Contains(string key)
        {
            return this.keyStore.ContainsAlias(key);
        }

        #endregion

        #region private methods
        private void Save()
        {
            lock (SaveLock)
            {
                using (var stream = new IsolatedStorageFileStream(StorageFile, FileMode.OpenOrCreate, FileAccess.Write, File))
                {
                    this.keyStore.Store(stream, this.protection.GetPassword());
                }
            }
        }
        #endregion

        #region Nested Types
        private class SecureData : Object, ISecretKey
        {
            private const string Raw = "RAW";

            private readonly byte[] data;

            public SecureData(byte[] dataBytes)
            {
                this.data = dataBytes;
            }

            #region IKey Members

            public string Algorithm => Raw;

            public string Format => Raw;

            public byte[] GetEncoded() => this.data;

            #endregion
        }
        #endregion
    }
}