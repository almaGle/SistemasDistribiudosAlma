package main

import (
	"encoding/json"
	"fmt"
	"net/http"
)
type Response struct{
	Status string `json: message`

}
func handler(w http.ResponseWriter, r *http.Request){
	w.Header().Set("Content-Type", "apliication/json");
	response:= Response {Status:"Hola, este es mi contenedor"}
	if err := json.NewEncoder(w).Encode(response); err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
	
	}
}
func main(){
	http.HandleFunc("/health", handler)
	fmt.Println("Staring server on: 8080")
	if err:= http.ListenAndServe(":8080", nil); err != nil{
		fmt.Println("Error starting server: ", err)
	}
}
