namespace FindIt.Application.ErrorHandling
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public Status? Status { get; private set; }

        protected Result(bool isSuccess, Status? status)
        {
            if ((isSuccess && status != Status.None) || (!isSuccess && status == Status.None))
            {
                throw new InvalidOperationException("Invalid state for result initialization.");
            }

            IsSuccess = isSuccess;
            Status = status;
        }

        public static Result Success() => new(true, Status.None);
        public static Result Failure(Status status) => new(false, status);

        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Status.None);
        public static Result<TValue> Failure<TValue>(Status status) => new(default, false, status);
    }

    public class Result<TValue> : Result
    {
        public TValue? Value { get; private set; }

        internal Result(TValue? value, bool isSuccess, Status? status) : base(isSuccess, status)
        {
            if (isSuccess && value == null)
            {
                throw new InvalidOperationException("Success results must have a value.");
            }
            if (!isSuccess && value != null)
            {
                throw new InvalidOperationException("Failure results cannot have a value.");
            }

            Value = value;
        }
    }
}