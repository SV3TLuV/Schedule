//
//  CurrentTimetable.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct CurrentTimetable: Codable, Hashable, Equatable {
    var groups: [Group]
    var groupNames: String
    var dates: [Grouping<Date, Timetable>]
}
