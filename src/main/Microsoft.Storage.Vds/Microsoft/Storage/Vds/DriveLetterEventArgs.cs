namespace Microsoft.Storage.Vds
{
    using System;

    public class DriveLetterEventArgs : EventArgs
    {
        private char letter;
        private Guid volumeId;

        public DriveLetterEventArgs(char letter, Guid volumeId)
        {
            this.letter = letter;
            this.volumeId = volumeId;
        }

        public char Letter
        {
            get
            {
                return this.letter;
            }
            set
            {
                this.letter = value;
            }
        }

        public Guid VolumeId
        {
            get
            {
                return this.volumeId;
            }
            set
            {
                this.volumeId = value;
            }
        }
    }
}

