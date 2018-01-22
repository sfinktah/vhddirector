namespace Microsoft.Storage.Vds
{
    using System;

    public class LunHints
    {
        private Microsoft.Storage.Vds.Hints hints;

        public LunHints()
        {
            this.hints = new Microsoft.Storage.Vds.Hints();
        }

        public LunHints(Microsoft.Storage.Vds.Hints hints)
        {
            this.hints = hints;
        }

        public ulong ExpectedMaximumSize
        {
            get
            {
                return this.hints.ExpectedMaximumSize;
            }
            set
            {
                this.hints.ExpectedMaximumSize = value;
            }
        }

        public bool FastCrashRecoveryRequired
        {
            get
            {
                return (this.hints.FastCrashRecoveryRequired == 1);
            }
            set
            {
                if (value)
                {
                    this.hints.FastCrashRecoveryRequired = 1;
                    this.hints.HintMask |= HintFlags.FastCrashRecoveryRequired;
                }
                else
                {
                    this.hints.FastCrashRecoveryRequired = 0;
                    this.hints.HintMask &= ~HintFlags.FastCrashRecoveryRequired;
                }
            }
        }

        public bool HardwareChecksumEnabled
        {
            get
            {
                return (this.hints.HardwareChecksumEnabled == 1);
            }
            set
            {
                if (value)
                {
                    this.hints.HardwareChecksumEnabled = 1;
                    this.hints.HintMask |= HintFlags.HardwareChecksumEnabled;
                }
                else
                {
                    this.hints.HardwareChecksumEnabled = 0;
                    this.hints.HintMask &= ~HintFlags.HardwareChecksumEnabled;
                }
            }
        }

        public Microsoft.Storage.Vds.Hints Hints
        {
            get
            {
                return this.hints;
            }
            set
            {
                this.hints = value;
            }
        }

        public bool IsYankable
        {
            get
            {
                return (this.hints.IsYankable == 1);
            }
            set
            {
                if (value)
                {
                    this.hints.IsYankable = 1;
                    this.hints.HintMask |= HintFlags.IsYankable;
                }
                else
                {
                    this.hints.IsYankable = 0;
                    this.hints.HintMask &= ~HintFlags.IsYankable;
                }
            }
        }

        public uint MaximumDriveCount
        {
            get
            {
                return this.hints.MaximumDriveCount;
            }
            set
            {
                this.hints.MaximumDriveCount = value;
            }
        }

        public bool MostlyReads
        {
            get
            {
                return (this.hints.MostlyReads == 1);
            }
            set
            {
                if (value)
                {
                    this.hints.MostlyReads = 1;
                    this.hints.HintMask |= HintFlags.MostlyReads;
                }
                else
                {
                    this.hints.MostlyReads = 0;
                    this.hints.HintMask &= ~HintFlags.MostlyReads;
                }
            }
        }

        public uint OptimalReadAlignment
        {
            get
            {
                return this.hints.OptimalReadAlignment;
            }
            set
            {
                this.hints.OptimalReadAlignment = value;
            }
        }

        public uint OptimalReadSize
        {
            get
            {
                return this.hints.OptimalReadSize;
            }
            set
            {
                this.hints.OptimalReadSize = value;
            }
        }

        public uint OptimalWriteAlignment
        {
            get
            {
                return this.hints.OptimalWriteAlignment;
            }
            set
            {
                this.hints.OptimalWriteAlignment = value;
            }
        }

        public uint OptimalWriteSize
        {
            get
            {
                return this.hints.OptimalWriteSize;
            }
            set
            {
                this.hints.OptimalWriteSize = value;
            }
        }

        public bool OptimizeForSequentialReads
        {
            get
            {
                return (this.hints.OptimizeForSequentialReads == 1);
            }
            set
            {
                if (value)
                {
                    this.hints.OptimizeForSequentialReads = 1;
                    this.hints.HintMask |= HintFlags.None | HintFlags.OptimizeForSequentialReads;
                }
                else
                {
                    this.hints.OptimizeForSequentialReads = 0;
                    this.hints.HintMask &= ~(HintFlags.None | HintFlags.OptimizeForSequentialReads);
                }
            }
        }

        public bool OptimizeForSequentialWrites
        {
            get
            {
                return (this.hints.OptimizeForSequentialWrites == 1);
            }
            set
            {
                if (value)
                {
                    this.hints.OptimizeForSequentialWrites = 1;
                    this.hints.HintMask |= HintFlags.None | HintFlags.OptimizeForSequentialWrites;
                }
                else
                {
                    this.hints.OptimizeForSequentialWrites = 0;
                    this.hints.HintMask &= ~(HintFlags.None | HintFlags.OptimizeForSequentialWrites);
                }
            }
        }

        public bool ReadBackVerifyEnabled
        {
            get
            {
                return (this.hints.ReadBackVerifyEnabled == 1);
            }
            set
            {
                if (value)
                {
                    this.hints.ReadBackVerifyEnabled = 1;
                    this.hints.HintMask |= HintFlags.None | HintFlags.ReadBackVerifyEnabled;
                }
                else
                {
                    this.hints.ReadBackVerifyEnabled = 0;
                    this.hints.HintMask &= ~(HintFlags.None | HintFlags.ReadBackVerifyEnabled);
                }
            }
        }

        public bool RemapEnabled
        {
            get
            {
                return (this.hints.RemapEnabled == 1);
            }
            set
            {
                if (value)
                {
                    this.hints.RemapEnabled = 1;
                    this.hints.HintMask |= HintFlags.None | HintFlags.RemapEnabled;
                }
                else
                {
                    this.hints.RemapEnabled = 0;
                    this.hints.HintMask &= ~(HintFlags.None | HintFlags.RemapEnabled);
                }
            }
        }

        public uint StripeSize
        {
            get
            {
                return this.hints.StripeSize;
            }
            set
            {
                this.hints.StripeSize = value;
            }
        }

        public bool WriteThroughCachingEnabled
        {
            get
            {
                return (this.hints.WriteThroughCachingEnabled == 1);
            }
            set
            {
                if (value)
                {
                    this.hints.WriteThroughCachingEnabled = 1;
                    this.hints.HintMask |= HintFlags.None | HintFlags.WriteThroughCachingEnabled;
                }
                else
                {
                    this.hints.WriteThroughCachingEnabled = 0;
                    this.hints.HintMask &= ~(HintFlags.None | HintFlags.WriteThroughCachingEnabled);
                }
            }
        }
    }
}

