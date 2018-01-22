using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VHD_Director
{
    public partial class About : Form
    {
        public SourceCredits credits = new SourceCredits();
        public About()
        {
            InitializeComponent();
            SourceCredit credit = new SourceCredit();
            credit.Product = "OpenStep style plist readier";
            credit.Author = "Lloyd Dupont";
            credit.Email = "lloyd@galador.net";
            credit.Url = "http://www.java2s.com/Open-Source/CSharp/GUI/CsGL/CsGL/Util/PList.cs.htm";
            credit.License = @"
 * BSD Licence:
 * Copyright (c) 2001, Lloyd Dupont (lloyd@galador.net)
 * <ORGANIZATION> 
 * All rights reserved.";
            credit.LicenseTerms = @"
 * Redistribution and use in source and binary forms, with or without 
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice, 
 * this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright 
 * notice, this list of conditions and the following disclaimer in the 
 * documentation and/or other materials provided with the distribution.
 * 3. Neither the name of the <ORGANIZATION> nor the names of its contributors
 * may be used to endorse or promote products derived from this software
 * without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS ""AS IS""
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE REGENTS OR CONTRIBUTORS BE LIABLE FOR
 * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
 * LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
 * OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
 * DAMAGE.";
            credits.Add(credit);

            credit = new SourceCredit();
            credit.Product = "BufferedGraphicsContext Example";
            credit.Author = "gsgc@stackoverflow.com";
            credit.Url = "http://stackoverflow.com/questions/835100/winforms-suspendlayout-resumelayout-is-not-enough";
            credits.Add(credit);

            credit = new SourceCredit();
            credit.Product = "Native WM_SETREDRAW Example";
            credit.Author = "ceztko@stackoverflow.com";
            credit.Url = "http://stackoverflow.com/questions/487661/how-do-i-suspend-painting-for-a-control-and-its-children";
            credits.Add(credit);

            credit = new SourceCredit();
            credit.Product = "Formatting a Drive using C# and WMI";
            credit.Author = "pappe82";
            credit.Url = "http://www.codeproject.com/KB/dotnet/cswmiformat.aspx";
            credits.Add(credit);

            credit = new SourceCredit();
            credit.Product = "SHFormatDrive API function API";
            credit.Author = "Nishant Sivakumar";
            credit.Url = "http://www.codeproject.com/KB/dialog/cformatdrivedialog.aspx";
            credits.Add(credit);

            credit = new SourceCredit();
            credit.Product = "Dynamic C# Evaluation";
            credit.Author = "Peter Wone";
            credit.Url = "http://stackoverflow.com/questions/760088/execute-a-string-in-c-sharp-4-0";
            credits.Add(credit);

            credits.Add("VDS enumeration of mounted VHDs", "Laurent Etiemble", "http://stackoverflow.com/questions/2755458/retrieving-virtual-disk-file-name-from-disk-number");


            
        }
    }

   
}
