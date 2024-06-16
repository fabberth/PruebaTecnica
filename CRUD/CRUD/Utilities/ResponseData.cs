namespace CRUD.Utilities
{
    /// <summary>
    /// Clase extructura para las repuestas al cliente
    /// </summary>
    public class ResponseData
    {
        public ResponseData(bool valueIsSuccess, string valueMessage = "")
        {
            IsSuccess = valueIsSuccess;
            Message = valueMessage;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
