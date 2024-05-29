using backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace backend.Services;


public class UserService 
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(IOptions<UserDatabaseSettings> userDatabaseSettings)
    {
        var mongoClient = new MongoClient(userDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(userDatabaseSettings.Value.DatabaseName);
        _userCollection = mongoDatabase.GetCollection<User>(userDatabaseSettings.Value.UserCollectionName);
    }

    public async Task<List<User>> GetAsync() => await _userCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) => await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser) => await _userCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, User updateUser) => await _userCollection.ReplaceOneAsync(x => x.Id == id, updateUser);

    public async Task RemoveAsync(string id) => await _userCollection.DeleteOneAsync(x => x.Id == id);
    
    public async Task<User?> GetByUsernameAsync(string username) => await _userCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
}