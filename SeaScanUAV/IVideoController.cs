using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaScanUAV
{
    public interface IVideoController
    {
        void OpenImageFile(String fileName);
        bool StartImageStream();
        void OpenImageCapture(int streamId);
        void TogglePauseVideo(bool pause);
        void StopImageStream();

        ImageStreamController ImageStream{get;}
    }
}
