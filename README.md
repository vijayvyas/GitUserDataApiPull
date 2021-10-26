# Git User Read App
>**Prerequisites**
Net core : 3.1 and above 

Need Redis Server follow the below Link
https://redis.io/topics/quickstart

## How To Use ## 
To Access **Redis Server** in the application redis Url need to set in the ``appsetting.CacheType`` file:
```xml
  "redisConnection": "localhost:6379",
 ```
### Change of Cache Configuration ###
 
App has support for InMemory Cache and Redis Cache both
To set the preferred Cache type in the ``appsetting.CacheType``, select one value from below list
```xml
["Redis", "InMemory"]
```
Default behavior is InMemory, in case this property is not set.

### Change of Cache Expiration ###
Application support configuration for Expiration time:
Modify the ``CacheTimeConfiguration`` value as mentioned below
```xml
"CacheTimeConfiguration": {
    "ExpirationInMinutes": 2
}
```

Application has support of for the Production Environment **Global Exception Handling** 
### Example ###
**HTTP Target Url**

```xml
https://localhost:44399/api/user 
-- 
Request type post
Content-Type : Application/json
```
Input Body json format:
```json
[
	"vijayvyas",
	"vijayvyas21212121212"
]
```
**Note:** Use the port as per your application launch setting.
