//
//  Discipline.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct Discipline: Identifiable, Codable, Hashable, Equatable {
    var id: Int
    var name: String
    var code: String
    var totalHours: Int
    var isDeleted: Bool
    var type: DisciplineType
    var term: Term?
    var speciality: Speciality?
}
