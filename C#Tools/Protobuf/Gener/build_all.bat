bin\protogen -i:%1.proto -o:cs/%1.cs -p:lightFramework -p:detectMissing -p:fixCase
bin\protoc --java_out=java %1.proto