using System;
using System.ComponentModel.DataAnnotations;

namespace Game_Step.Models.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Обязательно должно быть от 2 символов")]
        [StringLength(120, ErrorMessage = "Комментарий должен иметь минимум {2} и максимум {1} символов.", MinimumLength = 2)]
        public string Message { get; set; }

        public DateTime TimeCreated { get; set; }

        public bool IsCommentChecked { get; set; }
    }
}
