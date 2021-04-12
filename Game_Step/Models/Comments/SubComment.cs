namespace Game_Step.Models.Comments
{
    public class SubComment : Comment
    {
        public int MainCommentId { get; set; }
        public MainComment MainComment { get; set; }
    }
}
