using System;
using System.Collections.Generic;
using System.Text;
using Baisse.Study.DLL;

namespace Baisse.Study.BLL
{
    internal class StudyInfoBLL
    {
        internal static void SelectInfo(StudyCommon.Input.Istudy args)
        {
            StudyInfoDLL.SelectInfo(args);
        }
    }
}
