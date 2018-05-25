# ConfigWrapper	

Simple helper functions to retry something.

## Getting Started

Available on Nuget

### Prerequisites

.net 4.0 + 

## Use

Just call it, passing in the function or sub you want to retry and a number of retries.

```
int result = Retrier.OfT<int>(myFunction(1,2,3), 5);
/// retries 5 times
Retrier.Simple(myMethod("foo", "bar", 3);
/// retries 3 times.
```

## Authors

See the list of [contributors](https://github.com/brianbegy/ConfigWrapper/contributors) who participated in this project.

## License

This project is licensed under the MIT License 

## Acknowledgments

* Hat tip to the teams at Spotlite (RallyHealth) and Guaranteed Rate for their feedback.
