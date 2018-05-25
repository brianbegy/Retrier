# Retrier	

Simple helper functions to retry something.

## Getting Started

Available on Nuget

### Prerequisites

.net 4.0 + 

## Use

Just call it, passing in the function or method you want to retry and a number of retries.

```
public int myFunction(int a, int b, int c){
/// does something and returns an integer.
}

int result = Retrier.OfT<int>(()= {myFunction(1,2,3);}, 5);
/// retries 5 times

public void myMethod(string a, string b, int c){
/// does something and returns void.
}

Retrier.Simple(()=> {myMethod("foo", "bar");}, 3);
/// retries 3 times.
```
### Optional arguments

Both Simple and OfT take optional arguments for sleeping between retries.  The default is zero.

```
Retrier.Simple(()=>{myMethod("foo", "bar");}, 3, 1000);
/// retries 3 times, sleeping for 1000 ms between tries.
```

They also support incremental backoff

```
int result = Retrier.OfT<int>(()=> {myFunction(1,2,3);}, 5, new [] {500,1000,1500,2000,2500});
/// retries 5 times, sleeping for 500ms, then 1000ms, then 1500ms, then 2000ms, then 2500ms
```

## Authors

See the list of [contributors](https://github.com/brianbegy/ConfigWrapper/contributors) who participated in this project.

## License

This project is licensed under the MIT License 
