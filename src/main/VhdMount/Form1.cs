using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Drawing;

using System.Linq;

using System.Text;

using System.Windows.Forms;

using System.Runtime.InteropServices;



namespace VhdMount {


    public partial class Form1 : Form
    {



        // Setup the VHD_FLAGS enumeration.  Data copied from VHDMount.h

        enum VHD_FLAGS
        {

            VHD_NORMAL,

            VHD_NW_MAPPED,          // Unused

            VHD_MOUNT_AS_READONLY,  // Unused

            VHD_FORCE_UNMOUNT

        } ;



        // Create interop for the MountVHD call off of vhdmount.dll

        [DllImport("C:\\Program Files\\Microsoft Virtual Server\\Vhdmount\\vhdmount.dll", CharSet = CharSet.Auto)]

        static extern UInt32 MountVHD(String VHDFileName, UInt32 Flags);

        //DWORD WINAPI MountVHD(
        // IN LPCWSTR VHDFileName,
        // IN ULONG  Flags);

        // Create interop for the MountVHD call off of vhdmount.dll

        [DllImport("C:\\Program Files\\Microsoft Virtual Server\\Vhdmount\\vhdmount.dll", CharSet = CharSet.Auto)]

        static extern UInt32 UnmountVHD(String VHDFileName, UInt32 Flags);

        // The result code will always be '0' if the operation is successful.  Any other result code means a failure of some kind.  To look up the meaning of a result code go to here: http://msdn2.microsoft.com/en-us/library/ms681382(VS.85).aspx

        public Form1()
        {

            InitializeComponent();

        }



        // Mount the selected VHD when the user clicks on the MountButton

        private void MountButton_Click(object sender, EventArgs e)
        {

            UInt32 result;



            // Call MountVHD with the parameter of the text in the text box

            result = MountVHD(VHDTextBox.Text, (UInt32)VHD_FLAGS.VHD_NORMAL);



            // Handle result code

            if (result == 0)

                MessageBox.Show((char)34 + VHDTextBox.Text + (char)34 + " was successfully mounted.");

            else

                MessageBox.Show("An error was encountered attempting to mount " + (char)34 + VHDTextBox.Text + (char)34 + "." + (char)10 + (char)10 + "The error code returned was: " + result + ".");

        }



        // Unmount the selected VHD when the user clicks on the UnmountButton

        private void UnmountButton_Click(object sender, EventArgs e)
        {

            UInt32 result;



            // Call UnmountVHD with the parameter of the text in the text box

            result = UnmountVHD(VHDTextBox.Text, (UInt32)VHD_FLAGS.VHD_NORMAL);



            // Handle result code

            if (result == 0)

                MessageBox.Show((char)34 + VHDTextBox.Text + (char)34 + " was successfully unmounted.");

            else

                MessageBox.Show("An error was encountered attempting to unmount " + (char)34 + VHDTextBox.Text + (char)34 + "." + (char)10 + (char)10 + "The error code returned was: " + result + ".");

        }





    }

}

