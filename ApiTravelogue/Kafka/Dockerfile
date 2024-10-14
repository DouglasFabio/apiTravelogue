FROM bitnami/zookeeper:3.8-debian-11-r52

# Definir variáveis de ambiente
ENV KAFKA_ADVERTISED_LISTENERS PLAINTEXT://localhost:9092
ENV KAFKA_ZOOKEEPER_CONNECT zookeeper:2181

# Iniciar Kafka
CMD ["start-kafka.sh"]