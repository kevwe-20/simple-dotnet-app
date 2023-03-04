#!/bin/bash
# For installing Amplify
echo Installing curl first
sudo apt install curl
echo
echo Adding repository
curl -fsSL https://deb.nodesource.com/setup_14.x | sudo -E bash -
echo Repository added
echo
echo Installing nodejs and npm
sudo apt update 
sudo apt-get install -y nodejs
sudo apt-get install -y npm
echo
echo nodejs and npm are installed
echo
echo Installing Amplify
curl -sL https://aws-amplify.github.io/amplify-cli/install | bash && $SHELL
