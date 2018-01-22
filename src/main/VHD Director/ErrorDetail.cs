using System;
using System.Collections.Generic;
using System.Text;

namespace VHD_Director
{
    public class ErrorInformation
    {
        public String ErrorHeading;
        public String ErrorSubHeading;
        public String ErrorDetail;
        public String ErrorCategory;
        public Int16 ErrorSeverity;

        public ErrorInformation() { }
        public ErrorInformation(String ErrorHeading) { this.ErrorHeading = ErrorHeading; }
        public ErrorInformation(String ErrorHeading, String ErrorDetail) { this.ErrorHeading = ErrorHeading; this.ErrorDetail = ErrorDetail; }

    }
}
