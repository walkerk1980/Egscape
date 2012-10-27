#Egscape

Egscape is a new take on existing tools that are used to verify egress filtering. Think of it as a reverse port scanner. There are two peices, a client and sniffer (notice 'sniffer' is not server). Use this tool to test your outbound filtering. 


####Client
The client is built with .NET and implements three separate scanning types.

1. tcp: Attempt to initiate a socket connection to a server over tcp. Very much like nmap's connect scan. A clever hack is used to make this run fairly fast, sending one request every 50ms, this value can be editied in the source
2. udp: Attempt to initiate a socket connection to a server over udp. This is fast, because well udp doesn't give a damn.
3. proxy: This is where things get interesting. This will attempt to send a web request on every port to a destined server. This could be useful when you want to verify what ports are allowed through your web proxy. This uses a thread pool with 50 max threads and a timeout of 1000ms.

####Sniffer
The sniffer is written in python and requires scapy. Rather than taking the approach of existing tools and listening on every port for a connection from the client, we simply sniff the network for SYNs and UDP packets. In this manner we can use less resources and we don't have to have a server on every port.


####Client Syntax
Egscap-cli.exe \<scan type\> \<host\> \<port string\>

####Server Syntax
egscape-server.py \<interface\> \<client ip\>


