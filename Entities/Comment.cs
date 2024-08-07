using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AspNetCoreRestfulApi.Core.CoreEntity;
using AspNetCoreRestfulApi.Entity;

namespace AspNetCoreRestfulApi.Entities;
[Table("comment")]
public class Comment : BaseEntity
{
    [ForeignKey("user_id")]
    public User User { get; set; }
    
    [ForeignKey("post_id")]
    public Post Post { get; set; }
    public int post_id { get; set; }
    
    
    [Required]
    public  string Content { get; set; }
    
    [ForeignKey("comment_id")]
    [JsonIgnore]
    public Comment? ParentComment { get; set; }
    public int? comment_id { get; set; }
    
    public List<Comment> ChildrenComments { get; set; }

    public Boolean Status { get; set; }
    
    public Comment()
    {
    }

    public Comment(User user, Post post, int postId, string content, Comment? parentComment, int? commentId, List<Comment> childrenComments, bool status)
    {
        User = user;
        Post = post;
        post_id = postId;
        Content = content;
        ParentComment = parentComment;
        comment_id = commentId;
        ChildrenComments = childrenComments;
        Status = status;
    }

    public Comment(DateTime createdAt, DateTime updatedAt, User user, Post post, int postId, string content, Comment? parentComment, int? commentId, List<Comment> childrenComments, bool status) : base(createdAt, updatedAt)
    {
        User = user;
        Post = post;
        post_id = postId;
        Content = content;
        ParentComment = parentComment;
        comment_id = commentId;
        ChildrenComments = childrenComments;
        Status = status;
    }
}