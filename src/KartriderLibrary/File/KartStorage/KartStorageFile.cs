﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KartLibrary.File
{
    public class KartStorageFile : IRhoFile, IModifiableRhoFile
    {
        #region Members
        internal KartStorageFolder? _parentFolder;
        internal IModifiableRhoFile? _sourceFile;

        private string _name;
        private string _nameWithoutExt;
        private string _fullname;
        private IDataSource? _dataSource;

        private bool _disposed;
        #endregion

        #region Properties
        public KartStorageFolder? Parent => _parentFolder;

        IRhoFolder? IRhoFile.Parent => Parent;

        IModifiableRhoFolder? IModifiableRhoFile.Parent => Parent;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                if (_sourceFile is not null)
                    _sourceFile.Name = value;
                Regex fileNamePattern = new Regex(@"^(.*)\..*");
                Match match = fileNamePattern.Match(_name);
                if (match.Success)
                {
                    _nameWithoutExt = match.Groups[1].Value;
                }
                else
                {
                    _nameWithoutExt = _name;
                }
            }
        }

        public string FullName
        {
            get => Parent is not null ? $"{Parent.FullName}/{_name}" : _name;
        }

        public string NameWithoutExt => _nameWithoutExt;

        public int Size => _dataSource?.Size ?? 0;

        public IDataSource? DataSource
        {
            get => _dataSource;
            set
            {
                if (_sourceFile is not null)
                    _sourceFile.DataSource = value;
                _dataSource = value;
            }
        }
        #endregion

        #region Constructors
        public KartStorageFile()
        {
            _parentFolder = null;
            _name = "";
            _fullname = "";
            _nameWithoutExt = "";
            _dataSource = null;
            _sourceFile = null;
            _disposed = false;
        }

        internal KartStorageFile(IModifiableRhoFile sourceFile) : this()
        {
            _sourceFile = sourceFile;
            _dataSource = sourceFile.DataSource;
            Name = sourceFile.Name;
        }
        #endregion

        #region Methods
        public Stream CreateStream()
        {
            if (_dataSource is null)
                throw new InvalidOperationException("DataSource is null.");
            return _dataSource.CreateStream();
        }

        public void Dispose()
        {
            _parentFolder = null;
            if (_sourceFile is null)
                _dataSource?.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                }
            }
        }

        public override string ToString()
        {
            return $"KartStorageFile:{FullName}";
        }

        #endregion
    }
}
