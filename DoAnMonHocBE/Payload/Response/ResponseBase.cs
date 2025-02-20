namespace DoAnMonHocBE.PayLoad.Response
{
    public class ResponseBase
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public ResponseBase() { }

        public ResponseBase(int status, string message)
        {
            Status = status;
            Message = message;
        }


        public ResponseBase ResponseSuccess(string message)
        => new ResponseBase(StatusCodes.Status200OK, message);

        public ResponseBase ResponseError(int status, string message)
         => new ResponseBase(status, message);
    }
}
