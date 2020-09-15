docker run --name=consul-agent -e CONSUL_BIND_INTERFACE=eth0 -e 'CONSUL_LOCAL_CONFIG={"connect": { "enabled": true } }' consul:latest agent -node=client-1 -join=172.17.0.2
