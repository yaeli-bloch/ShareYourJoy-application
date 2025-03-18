using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.API.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;

        public UploadController(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        [HttpGet("presigned-url")]
        public async Task<IActionResult> GetPresignedUrl([FromQuery] string fileName)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = "yael-bucket-13269", 
                Key = fileName,                   // שם הקובץ (יכול להיות שם שיתקבל מהלקוח)
                Verb = HttpVerb.PUT,              // מבצע PUT (שימוש בהעלאת קובץ)
                Expires = DateTime.UtcNow.AddMinutes(5),  // הקישור יפוג אחרי 5 דקות
                ContentType = "image/jpeg" // סוג הקובץ - אפשר לשנות בהתאם לצורך
            };

            string url = _s3Client.GetPreSignedURL(request); // קבלת ה-URL
            return Ok(new { url }); // מחזירים את ה-URL
        }
    }
}
