function help {
    echo "Use this script as follows:"
    echo "  sh ./test.sh [options]\n"
    echo "Options are as follows:"
    echo "  -n: number -> number of times the api should be called.\n"
    echo "  -h: bool -> call for the help message.\n"
    exit {0}
}

function call {
    curl --request POST -H "Content-Type:application/json" http://localhost/orders --data "{\"ItemIds\": [\"burger\", \"fries\"]}" --silent 
}

NO_CALLS=1000

if [ -f "file.json" ]; then
   rm "file.json"
   echo ""file.json" is removed"
fi


echo "This is the script to call the prodtest api.\n"

# generate help message
while test $# -gt 0; do
    case $1 in
        -h|--help)
            help
        ;;
        -n)
            shift
            if test $# -gt 0; then
                echo "the -n value is: $1"
                NO_CALLS=$1
            else
                echo "No valid integer found after -n flag. Will use default (1000) as number of requests."
            fi
        ;;
        *)
            shift
        ;;
    esac
done

echo "API will be called $NO_CALLS times"


COUNT=0

echo "[" >> file.json

while test $COUNT -lt $NO_CALLS; do
    
    # echo "\nCall number $COUNT:"
    # function as defined above.
    output=$(call)
    # output=\'$output\'
    # echo $output
    echo "${output}" | jq >> file.json
    ((COUNT=COUNT+1))
    if test $COUNT -lt $NO_CALLS; then
        echo "," >> file.json
    fi
done

echo "]" >> file.json

echo "See file.json for results"
