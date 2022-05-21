using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Business.IRepository.ISharedRepository
{
    public interface IFileUploadRepository
    {
         Task<string> UploadImage(string folder, IBrowserFile file);
         void DeleteImage(string folder, string file);
    }
}