using System;
using System.Drawing;
using System.Threading.Tasks;

namespace RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage
{
    public interface IDownloadImage
    {
        Image Download();
    }
}