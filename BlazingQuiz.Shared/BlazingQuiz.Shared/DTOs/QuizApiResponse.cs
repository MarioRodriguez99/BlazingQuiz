namespace BlazingQuiz.Shared.DTOs
{
    public record QuizApiResponse(bool IsSuccess, string? Message)
    {
        public static QuizApiResponse Success() => new QuizApiResponse(true, null);
        public static QuizApiResponse Fail(string message) => new QuizApiResponse(false, message);
    }
}
