#!/bin/bash

function addHeader
{  
    sed $'1s/^/\\\n*\/\\\n\\\n#endregion\\\n\\\n/' $i >$i.new && mv $i.new $i     
    cat license-header-text $i >$i.new && mv $i.new $i
    sed $'1s/^/#region License\\\n\\\n\/*\\\n/' $i >$i.new && mv $i.new $i 
    #sed $'1s/^\xef\xbb\xbf//' $i >$i.new && mv $i.new $i
    sed 's'/`printf "\xef\xbb\xbf"`'//g' $i >$i.new && mv $i.new $i
}

function deleteHeader
{
    sed '/#region License/,/#endregion/d' $i >$i.new && mv $i.new $i 
    tail -n +2 $i >$i.new && mv $i.new $i     
}

count=0
for i in $(find ../src/ -name '*.cs' ! -name 'CommonAssemblyInfo.cs' ! -name 'Resource.designer.cs') 
do
  if [ "$1" == "add" ]
  then
    if ! grep -q '#region License' $i
    then
      addHeader
      count=$((count + 1))
    fi    
  fi

  if [ "$1" == "delete" ]
  then
    if grep -q '#region License' $i
    then
        deleteHeader
    fi    
  fi

  if [ "$1" == "update" ]
  then
    if grep -q '#region License' $i
    then
        deleteHeader
        addHeader
    fi    
  fi
done

if [ $count -gt 0 ]
then
  echo "Added license header to $count file(s). Please commit again."
  exit 1
else
  exit 0
fi

