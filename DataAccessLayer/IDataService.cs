using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;


namespace DataAccessLayer
{
    public interface IDataService
    {
        PagedList<Post> GetPosts(ResourceParameters resourceParameters);
        Post GetPost(int id);
        PagedList<Comment> GetComments(ResourceParameters resourceParameters);
        Comment GetComment(int id);
        PagedList<SovaUser> GetSovaUsers(ResourceParameters resourceParameters);
        SovaUser GetSovaUser(int id);
        void CreateSovaUser(SovaUser sovaUser);
        void UpdateSovaUser(SovaUser sovaUser);
        void DeleteSovaUser(SovaUser sovaUser);
        PagedList<History> GetHistories(ResourceParameters resourceParameters);
        History GetHistory(int userId, int postId);
        PagedList<User> GetUsers(ResourceParameters resourceParameters);
        User GetUser(int id);
        PagedList<Tag> GetTags(ResourceParameters resourceParameters);
        Tag GetTag(int id);
        void CreateHistory(History history);
        void UpdateHistory(History history);
        void DeleteHistory(History history);
        IList<Posttype> GetPosttypes();
        Posttype GetPosttype(int id);
        int GetSovaUserCount();

    }
}
