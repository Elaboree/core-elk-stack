# Elasticsearch stack (ELK) with docker-compose and Serilog

It gives us the ability to analyze any data set by using the searching/aggregation capabilities of Elasticsearch and
the visualization power of Kibana.

Based on the official Docker images from Elastic:

* [Elasticsearch](https://github.com/elastic/elasticsearch)
* [Kibana](https://github.com/elastic/kibana)
* [Serilog](https://github.com/serilog/serilog)


### Host setup
By default, the stack exposes the following ports:
* 5000: Logstash TCP input
* 9200: Elasticsearch HTTP
* 9300: Elasticsearch TCP transport
* 5601: Kibana

## Usage

```console
$ docker-compose up
```

To run all services in the background, you can add the -d flag to the command.

```console
$ docker-compose up -d 
```

### Cleanup

By default, Elasticsearch data is stored in a volume. 


If you want to completely shut down the stack and remove all data, you can use the following Docker Compose command:

```console
$ docker-compose down -v
```


