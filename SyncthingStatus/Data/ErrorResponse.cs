namespace SyncthingStatus.Data
{
    class ErrorResponse
    {
        public Error[] Errors { get; set; }

        internal class Error
        {
            public System.DateTime When { get; set; }
            public string Message { get; set; }
        }
    }
}
