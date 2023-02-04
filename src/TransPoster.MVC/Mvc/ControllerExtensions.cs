using Microsoft.AspNetCore.Mvc;

namespace TransPoster.MVC.Mvc;

public static class ControllerExtensions
{
    public static JsonResult JsonDefaultContract(this Controller controller, object data)
        => controller.Json(data, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNamingPolicy = null
        });

}
