//
//  TimeType.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct TimeType: Identifiable, Codable, Hashable, Equatable {
    var id: Int
    var name: String
    var isDeleted: Bool
}
