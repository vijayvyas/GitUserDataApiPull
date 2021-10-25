namespace SampleAppTest
{
    public interface ICacheService
    {
        void updateCache<T>(string key, T Value);

        T getFromCache<T>(string key);
    }
}
