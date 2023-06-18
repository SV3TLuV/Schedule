//
//  Date.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct Date: Identifiable, Codable, Hashable, Equatable {
    var id: Int
    var term: Int
    var value: String
    var day: Day
    var weekType: WeekType
}
