using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Reflection;


namespace CSharp.cc.Registry
{
    public class Key
    {
        public String KeyPath { get; set; }
        public RegistryKey BaseRegistryKey { get; set; }
        public RegistryKey SubRegistryKey { get; set; }
        public String SubRegistryKeyName { get; set; }
        /// <summary>
        /// The name portion of the key path
        /// </summary>
        public String KeyName { get; set; }
        public Object KeyValue { get; set; }
        public RegistryValueKind KeyValueKind { get; set; }
        public Boolean IsValid
        {
            get
            {
                // TODO: Take into account that reading a key that isn't set, may or may not result in a valid value,
                // depending on whether the user has specified a default.  Since KeyValueKind should be null unless
                // a request has been made, then we should record the status of validity at that point.
                //
                // UNLESS we want to allow the caller to write a key without attempting a read first (probably a bad idea)
                return (_Valid && BaseRegistryKey != null && SubRegistryKey != null && KeyValueKind != null) ? true : false;
            }
        }
        public enum ErrorCode
        {
            None,
            InvalidKeyPath,
            InvalidBaseKey,
            InvalidSubKey,
            NoKeyValue,
            UnknownException,
            NoKeyValueKind
        };
        protected string _keyPath;
        protected object _defaultValue;
        protected RegistryValueKind _defaultValueKind = RegistryValueKind.Unknown;

        protected Boolean _Valid = false;
        protected ErrorCode _ErrorStatus = ErrorCode.None;





        /// <summary>
        /// Get Registry value from \HKEY_\X\Y\Z\k 
        /// </summary>
        /// <param name="keyPath"></param>
        public bool TryGetKey(string keyPath)
        {
            return TryGetKey(keyPath, null, RegistryValueKind.Unknown);
        }

        /// <summary>
        /// Get Registry value from \HKEY_\X\Y\Z\k 
        /// </summary>
        /// <param name="keyPath">\HKEY_\X\Y\Z\k</param>
        /// <param name="defaultValue"></param>
        /// <param name="defaultKind"></param>
        public bool TryGetKey(string keyPath, object defaultValue, RegistryValueKind defaultValueKind)
        {
            _defaultValue = defaultValue;
            _defaultValueKind = defaultValueKind;
            _keyPath = keyPath;
            return TryParseKeyPath(keyPath, false) ? GetKey() : false;
        }


        public void SetKey(object value)
        {
            if (!IsValid)
            {
                throw new Exception("Key has not been made Valid by a read");
            }

            if (!TryParseKeyPath(KeyPath, true))
            {
                // _Valid = false;
                throw new Exception("Couldn't parse key for write access");
            }


            SubRegistryKey.SetValue(KeyName, value, KeyValueKind);
        }

        /// <summary>
        /// Attempt to process a path to a registry value
        /// </summary>
        /// <param name="registryPath">\HKEY_\X\Y\Z\k</param>
        /// <returns>false on failure and sets ErrorCode</returns>
        protected bool TryParseKeyPath(string registryPath, bool writable)
        {

            _Valid = false;
            KeyPath = registryPath;

            // HKEY_LOCAL_MACHINE\SOFTWARE\blah\blah\keyname
            // http://www.codeproject.com/KB/system/modifyregistry.aspx

            string[] keys = registryPath.Split('\\');
            if (keys.Length < 3)
            {
                _ErrorStatus = ErrorCode.InvalidKeyPath;
                return false;
            }

            String tmpBaseKeyName;

            RegistryKey tmpSubRegistryKey;
            String tmpSubRegistryKeyName;


            String tmpKeyName;
            RegistryKey tmpBaseRegistryKey;


            tmpBaseKeyName = keys[0];
            tmpBaseRegistryKey = RegistryKeyFromString(tmpBaseKeyName);
            if (tmpBaseRegistryKey == null)
            {
                _ErrorStatus = ErrorCode.InvalidBaseKey;
                return false;
            }


            tmpSubRegistryKeyName = string.Join("\\", keys, 1, keys.Length - 2);
            // Open a subKey as read-only
            tmpSubRegistryKey = tmpBaseRegistryKey.OpenSubKey(tmpSubRegistryKeyName, writable);
            if (tmpSubRegistryKey == null)
            {
                _ErrorStatus = ErrorCode.InvalidSubKey;
                return false;
            }

            tmpKeyName = keys[keys.Length - 1];

            BaseRegistryKey = tmpBaseRegistryKey;
            SubRegistryKey = tmpSubRegistryKey;
            KeyName = tmpKeyName;
            // KeyValueKind = tmpKeyValueKind;
            // KeyValue = tmpKeyValue;

            return true;
        }

        public bool GetKey() {
            _Valid = false;

            RegistryValueKind tmpKeyValueKind;
            object tmpKeyValue;

            try
            {
                // This splitting of requests really only exists to stop people requesting with a default value without a default type.
                // Although this is valid behaviour for the Win32 API which will just case most things to string if unsure, we wish
                // to be a little more discerning.

                if (_defaultValue != null && _defaultValueKind != RegistryValueKind.Unknown)
                {
                    tmpKeyValue = SubRegistryKey.GetValue(KeyName, _defaultValue, RegistryValueOptions.DoNotExpandEnvironmentNames);
                }
                else
                {
                    tmpKeyValue = SubRegistryKey.GetValue(KeyName, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
                }

                if (tmpKeyValue == null)
                {
                    _ErrorStatus = ErrorCode.NoKeyValue;
                    return false;
                }

                // TODO: We don't actually know what the behaviour of this will be, if requested on a non-existent key.
                tmpKeyValueKind = RegistryValueKind.Unknown;
                try
                {
                    tmpKeyValueKind = SubRegistryKey.GetValueKind(KeyName);
                    if (tmpKeyValueKind == RegistryValueKind.Unknown)
                    {
                        tmpKeyValueKind = RegistryValueKind.Unknown;
                        Console.WriteLine("tmpKeyValueKind==Unknown");
                    }
                }
                catch (Exception ex)
                {
                    tmpKeyValueKind = RegistryValueKind.Unknown;
                    Console.WriteLine("tmpKeyValueKind exception: {0}", ex.Message);
                }

            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine("Exception: {0}", ex.Message);
#endif
                _ErrorStatus = ErrorCode.UnknownException;
                return false;
            }

            if (tmpKeyValueKind == RegistryValueKind.Unknown)
            {
                // This shouldn't really be an error always, but we will make it one until we work out
                // exactly how it behaves.
                _ErrorStatus = ErrorCode.NoKeyValueKind;
                return false;
            }

            // If we get here, then all the values were good, and we should update the class properties
            
            KeyValueKind = tmpKeyValueKind;
            KeyValue = tmpKeyValue;

            _Valid = true;
            return true;

            // \BaseRegistryKey\SubRegistryKey(\..\..)\KeyName = (KeyValueKind)KeyValue
        }

        /// <summary>
        /// Get RegistryKey from string like HKEY_LOCAL_MACHINE
        /// </summary>
        /// <param name="basename">HKEY_whatever</param>
        /// <returns>The matching RegistryKey</returns>
        static public RegistryKey RegistryKeyFromString(string basename)
        {
            Type type = typeof(Microsoft.Win32.Registry); // Get type pointer
            FieldInfo[] fields = type.GetFields(); // Obtain all fields
            foreach (var field in fields) // Loop through fields
            {
                string name = field.Name; // Get string name
                object temp = field.GetValue(null); // Get value
                if (temp is Microsoft.Win32.RegistryKey) // See if it is an integer.
                {
                    string keyName = ((Microsoft.Win32.RegistryKey)temp).Name;
                    if (keyName.Equals(basename.ToUpper()))
                    {
                        return (Microsoft.Win32.RegistryKey)(temp);
                    }
                }
            }

            return null;
        }

    }


    public class RegistryType
    {
        public string EnumName { get; set; }
        public string RegEditName { get; set; }
        public RegistryType(string enumName, string regEditName)
        {
            this.EnumName = enumName;
            this.RegEditName = regEditName;
        }
        public override string ToString()
        {
            return this.RegEditName;
        }
    }

    public class RegistryTools
    {
        public static List<RegistryType> GetRegistryTypeList()
        {
            RegistryTypeList rtl = new RegistryTypeList();
            return rtl.GetList();
        }
    }
    public class RegistryTypeList
    {
        protected List<RegistryType> RegistryTypes = new List<RegistryType>();

        protected void Add(string enumName, string regEditName)
        {
            RegistryType newType = new RegistryType(enumName, regEditName);
            RegistryTypes.Add(newType);
        }

        protected void MakeList()
        {

            Add("String", "REG_SZ");
            Add("ExpandString", "REG_EXPAND_SZ");
            Add("Binary", "REG_BINARY");
            Add("DWord", "REG_DWORD");
            Add("MultiString", "REG_MULTI_SZ");
            Add("QWord", "REG_QWORD");
            Add("Unknown", "REG_OTHER");
        }

        public RegistryTypeList()
        {
            MakeList();
        }

        public List<RegistryType> GetList()
        {
            return this.RegistryTypes;
        }
    }
}