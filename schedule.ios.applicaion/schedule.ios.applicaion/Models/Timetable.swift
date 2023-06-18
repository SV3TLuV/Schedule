//
//  Timetable.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct Timetable: Identifiable, Codable, Hashable, Equatable {
    var id: Int
    var date: Date
    var groups: [Group]
    var groupNames: String
    var lessons: [Lesson]
}
