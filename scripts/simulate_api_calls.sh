#!/bin/bash

help() {
    echo "Use this script as follows:"
    echo "  sh path/to/simulate_api_calls.sh [options]\n"
    echo "Options are as follows:"
    echo "  -n: number -> number of times the api should be called.\n"
    echo "  -h: bool -> call for the help message.\n"
    exit {0}
}

call() {
    curl --request POST -H "Content-Type:application/json" http://localhost/orders --data "{\"ItemIds\": [\"burger\", \"fries\"]}" --silent 
}

NO_CALLS=1000
RESPONSE_FILE="temp/request_output.json"

if [ -f $RESPONSE_FILE ]; then
   rm $RESPONSE_FILE
   echo "File: $RESPONSE_FILE is removed"
fi


echo "This is the script to call the prodtest api.\n"

# Check options
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

echo "[" >> $RESPONSE_FILE

# call api NO_CALLS times and append response to RESPONSE_FILE
while test $COUNT -lt $NO_CALLS; do
    # function as defined above.
    output=$(call)

    echo "${output}" | jq >> $RESPONSE_FILE
    ((COUNT=COUNT+1))
    if test $COUNT -lt $NO_CALLS; then
        echo "," >> $RESPONSE_FILE
    fi
done

echo "]" >> $RESPONSE_FILE

echo "See '$RESPONSE_FILE' for results"
