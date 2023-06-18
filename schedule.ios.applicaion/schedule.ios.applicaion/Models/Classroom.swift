//
//  Classroom.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct Classroom: Identifiable, Codable, Hashable, Equatable {
    var id: Int
    var cabinet: String
    var isDeleted: Bool
}
