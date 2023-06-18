//
//  ScheduleApi.swift
//  schedule.ios.applicaion
//
//  Created by Иван Светлов on 18.06.2023.
//

import Foundation
import SwiftHttp

class ScheduleApi: HttpCodablePipelineCollection {
    let client: HttpClient = UrlSessionHttpClient(logLevel: .info)
    let baseUrl = HttpUrl(url: URL(string: "http://192.168.1.158:5000")!)!

    func fetchCurrentTimetables(groupId: Int? = nil, dateCount: Int = 2) async throws -> PagedList<CurrentTimetable> {
        try await decodableRequest(
            executor: client.dataTask,
            url: baseUrl.path("api/timetable/current").query([
                "groupId": groupId != nil ? String(groupId!) : nil,
                "dateCount": String(dateCount),
            ]),
            method: .get
        )
    }
    
    func fetchGroups(page: Int = 1, pageSize: Int = 20, search: String = "") async throws -> PagedList<Group> {
        try await decodableRequest(
            executor: client.dataTask,
            url: baseUrl.path("api/group").query([
                "page": String(page),
                "pageSize": String(pageSize),
                "search": String(search),
            ]),
            method: .get
        )
    }
}
