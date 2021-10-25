namespace SampleAppTest
{
    public interface ICacheService<T> where T:class
    {
        void updateCache(string key, T userModel);

        T getFromCache(string key);
    }
}
