#!/bin/sh
if [[ $# -gt 0 ]] 
then
	sh ./license-header.sh add
	exitCode=$?

	cd ..

	if [[ $exitCode != 0 ]]
	then 
	   git commit -a -m "automated addition of header license info"
	   git push origin HEAD
	fi
fi	