#!/bin/bash
docker stop pokedex
docker rm pokedex
docker run -d --name pokedex -p 5000:80 -p 5001:443 \
        -e ASPNETCORE_URLS="https://+;http://+" \
        -e "ASPNETCORE_HTTPS_PORT=5001" \
        -e ASPNETCORE_Kestrel__Certificates__Default__Password="123456" \
        -e ASPNETCORE_Kestrel__Certificates__Default__Path="/https/pokedex.pfx" \
		-v $(pwd)/pokedexapp:/https/ \
		pokedex
