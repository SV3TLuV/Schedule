package com.example.schedulemobile.presentation.viewModels

import com.microsoft.signalr.HubConnection
import com.microsoft.signalr.HubConnectionBuilder
import com.microsoft.signalr.TransportEnum
import javax.inject.Inject

class SignalRClient @Inject constructor() {
    lateinit var hubConnection: HubConnection

    init {
        startSignalRConnection()
    }

    private fun startSignalRConnection() {
        try {
            hubConnection = HubConnectionBuilder.create("http://192.168.0.3:5291/hub/notification")
                .withTransport(TransportEnum.WEBSOCKETS)
                .build()

            hubConnection.start().blockingAwait()

            /*hubConnection.on("notified", { objName ->
                if (objName == "Timetable") {

                }
            }, Any::class.java)*/
        } catch (e: Exception) {
            e.printStackTrace()
        }
    }
}