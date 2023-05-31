package friendlyPusses.common.kafka;

import friendlyPusses.common.exceptions.ServiceException;
import org.apache.kafka.clients.consumer.ConsumerRecord;
import org.apache.kafka.clients.producer.ProducerRecord;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.kafka.core.KafkaTemplate;
import org.springframework.kafka.requestreply.ReplyingKafkaTemplate;
import org.springframework.kafka.requestreply.RequestReplyFuture;
import org.springframework.kafka.support.SendResult;
import org.springframework.stereotype.Service;

import java.util.concurrent.ExecutionException;

@Service
public class KafkaService {
    private final ReplyingKafkaTemplate<String, Object, Object> replyingKafka;
    private final KafkaTemplate<String, Object> kafka;

    @Autowired
    public KafkaService(
            ReplyingKafkaTemplate<String, Object, Object> replyingKafka,
            KafkaTemplate<String, Object> kafka) {
        this.replyingKafka = replyingKafka;
        this.kafka = kafka;
    }

    public <T> T send(Object request, String topic, Class<T> awaitedType) {
        try {
            final ConsumerRecord<String, Object> consumerRecord;

            ProducerRecord<String, Object> record = new ProducerRecord<>(topic, request);
            RequestReplyFuture<String, Object, Object> replyFuture = replyingKafka.sendAndReceive(record);
            SendResult<String, Object> sendResult = replyFuture.getSendFuture().get();
            consumerRecord = replyFuture.get();

            return awaitedType.cast(consumerRecord.value());
        }
        catch (Exception e) {
            throw new ServiceException("Something went wrong while sanding kafka message");
        }
    }

    public void sendAsync(Object request, String topic) {
        kafka.send(topic, request);
    }
}
